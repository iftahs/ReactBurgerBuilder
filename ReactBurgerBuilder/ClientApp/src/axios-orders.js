import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://burger-builder-c0bb6.firebaseio.com/'
});

export default instance;