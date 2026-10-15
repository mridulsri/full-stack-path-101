import { useSelector, useDispatch } from "react-redux"
import type { RootState } from "../lib/store"
import { removeFromCart, updateQuantity } from "../features/cart/cartSlice"
import { Button } from "../components/ui/button"
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from "../components/ui/card"
import { Input } from "../components/ui/input"

export function Cart() {
  const cart = useSelector((state: RootState) => state.cart.items)
  const dispatch = useDispatch()

  const total = cart.reduce(
    (sum, item) =>
      sum +
      (item.basePrice + item.toppings.reduce((toppingSum, topping) => toppingSum + topping.price, 0)) * item.quantity,
    0,
  )

  return (
    <Card>
      <CardHeader>
        <CardTitle>Your Cart</CardTitle>
      </CardHeader>
      <CardContent>
        {cart.length === 0 ? (
          <p>Your cart is empty</p>
        ) : (
          <ul>
            {cart.map((item) => (
              <li key={item.id} className="mb-4">
                <div className="flex justify-between items-center">
                  <span>{item.name}</span>
                  <span>
                    $
                    {(
                      (item.basePrice + item.toppings.reduce((sum, topping) => sum + topping.price, 0)) *
                      item.quantity
                    ).toFixed(2)}
                  </span>
                </div>
                {item.toppings.length > 0 && (
                  <ul className="ml-4 text-sm text-gray-600">
                    {item.toppings.map((topping, toppingIndex) => (
                      <li key={toppingIndex}>
                        {topping.name} (+${topping.price.toFixed(2)})
                      </li>
                    ))}
                  </ul>
                )}
                <div className="flex items-center mt-2">
                  <Input
                    type="number"
                    min="1"
                    value={item.quantity}
                    onChange={(e) =>
                      dispatch(updateQuantity({ id: item.id, quantity: Number.parseInt(e.target.value) }))
                    }
                    className="w-20 mr-2"
                  />
                  <Button variant="destructive" size="sm" onClick={() => dispatch(removeFromCart(item.id))}>
                    Remove
                  </Button>
                </div>
              </li>
            ))}
          </ul>
        )}
      </CardContent>
      <CardFooter className="flex justify-between">
        <span className="font-bold">Total: ${total.toFixed(2)}</span>
        <Button disabled={cart.length === 0}>Checkout</Button>
      </CardFooter>
    </Card>
  )
}

