import React, { useState } from 'react';
import { PizzaList } from './components/PizzaList';
import { Cart } from './components/Cart';
import { Checkout } from './components/Checkout';

// Define the Pizza type
export type Pizza = {
  id: number;
  name: string;
  price: number;
  image: string;
};

// Sample pizza data
const pizzas: Pizza[] = [
  { id: 1, name: 'Margherita', price: 10, image: '/placeholder.svg?height=100&width=100' },
  { id: 2, name: 'Pepperoni', price: 12, image: '/placeholder.svg?height=100&width=100' },
  { id: 3, name: 'Vegetarian', price: 11, image: '/placeholder.svg?height=100&width=100' },
  { id: 4, name: 'Hawaiian', price: 13, image: '/placeholder.svg?height=100&width=100' },
];

export default function App() {
  const [cart, setCart] = useState<Pizza[]>([]);
  const [isCheckout, setIsCheckout] = useState(false);

  const addToCart = (pizza: Pizza) => {
    setCart([...cart, pizza]);
  };

  const removeFromCart = (pizzaId: number) => {
    setCart(cart.filter((item) => item.id !== pizzaId));
  };

  const total = cart.reduce((sum, item) => sum + item.price, 0);

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-3xl font-bold mb-4">Pizza Store</h1>
      {!isCheckout ? (
        <div className="flex flex-col md:flex-row">
          <div className="w-full md:w-2/3 pr-4">
            <PizzaList pizzas={pizzas} addToCart={addToCart} />
          </div>
          <div className="w-full md:w-1/3 mt-4 md:mt-0">
            <Cart cart={cart} removeFromCart={removeFromCart} total={total} setIsCheckout={setIsCheckout} />
          </div>
        </div>
      ) : (
        <Checkout total={total} setIsCheckout={setIsCheckout} />
      )}
    </div>
  );
}

