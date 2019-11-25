import { put, delay } from 'redux-saga/effects';
import axios from 'axios';

import * as actions from '../actions/index';

export function* logoutSaga(action) {
    yield localStorage.removeItem('token');
    yield localStorage.removeItem('expirationTime');
    yield localStorage.removeItem('userId');
    yield put(actions.logoutSucceed());
}

export function* checkAuthTimeoutSaga(action) {
    yield delay(action.expirationTime * 1000);
    yield put(actions.logout());
}

export function* authUserSaga(action) {
    yield put(actions.authStart());
    const authData = {
        email: action.email,
        password: action.password,
        returnSecureToken: true
    };
    let url = 'https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=AIzaSyCrrvmV5v4tStUx9qG_5IzOlXus62UXFrQ';
    if (!action.isSignup)
        url = 'https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=AIzaSyCrrvmV5v4tStUx9qG_5IzOlXus62UXFrQ';

    try {
        const res = yield axios.post(url, authData)
        const expirationDate = yield new Date(new Date().getTime() + res.data.expiresIn * 1000);
        yield localStorage.setItem('token', res.data.idToken);
        yield localStorage.setItem('expirationTime', expirationDate);
        yield localStorage.setItem('userId', res.data.localId);

        yield put(actions.authSuccess(res.data.idToken, res.data.localId));
        yield put(actions.checkAuthTimeout(res.data.expiresIn));
    }
    catch (err) {
        yield put(actions.authFail(err.response.data.error));
    };
}

export function* authCheckStateSaga(action) {
    const token = yield localStorage.getItem('token');
    if (!token) {
        yield put(actions.logout());
    }
    else {
        const expirationTime = yield new Date(localStorage.getItem('expirationTime'));
        if (expirationTime > new Date()) {
            const userId = yield localStorage.getItem('userId');
            yield put(actions.authSuccess(token, userId));
            yield put(actions.checkAuthTimeout((expirationTime.getTime() - new Date().getTime()) / 1000));
        }
        else {
            yield put(actions.logout());
        }
    }
}