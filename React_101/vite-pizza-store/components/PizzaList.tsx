import type { Pizza, Topping } from "@/types"
import { PizzaItem } from "./PizzaItem"

type PizzaListProps = {
  pizzas: Pizza[]
  availableToppings: Topping[]
}

export function PizzaList({ pizzas, availableToppings }: PizzaListProps) {
  return (
    <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
      {pizzas.map((pizza) => (
        <PizzaItem key={pizza.id} pizza={pizza} availableToppings={availableToppings} />
      ))}
    </div>
  )
}

