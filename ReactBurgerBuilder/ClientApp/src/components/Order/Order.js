import React from 'react';
import classes from './Order.css';

const order = (props) => {
    const ingredients = props.ingredients.map(ingredient => (
        <li
            key={ingredient.id}
            style={{
                textTransform: 'capitalize',
                display: 'inline-block',
                margin: '0 8px',
                border: '1px solid #ccc',
                padding: '5px'
            }}>{ingredient.name}: ({ingredient.amount})</li>    
    ));

    return (
        <div className={classes.Order}>
            <p>Ingredients:</p>
            <ul>
                {ingredients}
            </ul>
            <p>Price: <strong>{props.totalPrice}$</strong></p>
        </div>
    );
}

export default order;