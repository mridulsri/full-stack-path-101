"use client"
import { PizzaList } from "./PizzaList"
import { Cart } from "./Cart"
import { useGetPizzasQuery, useGetToppingsQuery } from "@/services/pizzaApi"

export default function PizzaStore() {
  const { data: pizzas, isLoading: isPizzasLoading } = useGetPizzasQuery()
  const { data: toppings, isLoading: isToppingsLoading } = useGetToppingsQuery()

  if (isPizzasLoading || isToppingsLoading) {
    return <div>Loading...</div>
  }

  if (!pizzas || !toppings) {
    return <div>Error loading data</div>
  }

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-3xl font-bold mb-4">Pizza Store</h1>
      <div className="flex flex-col md:flex-row">
        <div className="w-full md:w-2/3 pr-4">
          <PizzaList pizzas={pizzas} availableToppings={toppings} />
        </div>
        <div className="w-full md:w-1/3 mt-4 md:mt-0">
          <Cart />
        </div>
      </div>
    </div>
  )
}

