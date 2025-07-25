import { useState } from "react"
import { useDispatch } from "react-redux"
import type { Pizza, Topping } from "@/types"
import { addToCart } from "@/features/cart/cartSlice"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "@/components/ui/card"
import { Checkbox } from "@/components/ui/checkbox"
import { Label } from "@/components/ui/label"
import Image from "next/image"

type PizzaItemProps = {
  pizza: Pizza
  availableToppings: Topping[]
}

export function PizzaItem({ pizza, availableToppings }: PizzaItemProps) {
  const [selectedToppings, setSelectedToppings] = useState<Topping[]>([])
  const dispatch = useDispatch()

  const toggleTopping = (topping: Topping) => {
    setSelectedToppings((prevToppings) =>
      prevToppings.some((t) => t.id === topping.id)
        ? prevToppings.filter((t) => t.id !== topping.id)
        : [...prevToppings, topping],
    )
  }

  const totalPrice = pizza.basePrice + selectedToppings.reduce((sum, topping) => sum + topping.price, 0)

  const handleAddToCart = () => {
    dispatch(
      addToCart({
        ...pizza,
        toppings: selectedToppings,
      }),
    )
  }

  return (
    <Card>
      <CardHeader>
        <CardTitle>{pizza.name}</CardTitle>
      </CardHeader>
      <CardContent>
        <Image src={pizza.image || "/placeholder.svg"} alt={pizza.name} width={100} height={100} className="mb-2" />
        <p className="font-bold mb-2">Base Price: ${pizza.basePrice.toFixed(2)}</p>
        <div className="space-y-2">
          <p className="font-semibold">Customize your toppings:</p>
          {availableToppings.map((topping) => (
            <div key={topping.id} className="flex items-center space-x-2">
              <Checkbox
                id={`topping-${pizza.id}-${topping.id}`}
                checked={selectedToppings.some((t) => t.id === topping.id)}
                onCheckedChange={() => toggleTopping(topping)}
              />
              <Label htmlFor={`topping-${pizza.id}-${topping.id}`}>
                {topping.name} (+${topping.price.toFixed(2)})
              </Label>
            </div>
          ))}
        </div>
        <p className="font-bold mt-2">Total Price: ${totalPrice.toFixed(2)}</p>
      </CardContent>
      <CardFooter>
        <Button onClick={handleAddToCart}>Add to Cart</Button>
      </CardFooter>
    </Card>
  )
}

