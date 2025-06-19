export interface Topping {
  id: number
  name: string
  price: number
}

export interface Pizza {
  id: number
  name: string
  basePrice: number
  image: string
  toppings: Topping[]
}

export interface CartItem extends Pizza {
  quantity: number
}

