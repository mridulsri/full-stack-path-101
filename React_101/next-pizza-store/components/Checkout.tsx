import React, { useState } from 'react'
import { Button } from '@/components/ui/button'
import { Card, CardContent, CardFooter, CardHeader, CardTitle } from '@/components/ui/card'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'

type CheckoutProps = {
  total: number
  setIsCheckout: (isCheckout: boolean) => void
}

export function Checkout({ total, setIsCheckout }: CheckoutProps) {
  const [name, setName] = useState('')
  const [address, setAddress] = useState('')

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    // Here you would typically process the order
    alert(`Thank you for your order, ${name}! Your total is $${total.toFixed(2)}`)
    setIsCheckout(false)
  }

  return (
    <Card className="w-full max-w-md mx-auto">
      <CardHeader>
        <CardTitle>Checkout</CardTitle>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div>
            <Label htmlFor="name">Name</Label>
            <Input
              id="name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              required
            />
          </div>
          <div>
            <Label htmlFor="address">Address</Label>
            <Input
              id="address"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
              required
            />
          </div>
          <p className="font-bold">Total: ${total.toFixed(2)}</p>
        </form>
      </CardContent>
      <CardFooter className="flex justify-between">
        <Button variant="outline" onClick={() => setIsCheckout(false)}>
          Back to Cart
        </Button>
        <Button type="submit" onClick={handleSubmit}>
          Place Order
        </Button>
      </CardFooter>
    </Card>
  )
}

