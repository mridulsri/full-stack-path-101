import { NextResponse } from "next/server"
import type { Pizza } from "@/types"

const pizzas: Pizza[] = [
  { id: 1, name: "Margherita", basePrice: 10, image: "/placeholder.svg?height=100&width=100", toppings: [] },
  { id: 2, name: "Pepperoni", basePrice: 12, image: "/placeholder.svg?height=100&width=100", toppings: [] },
  { id: 3, name: "Vegetarian", basePrice: 11, image: "/placeholder.svg?height=100&width=100", toppings: [] },
  { id: 4, name: "Hawaiian", basePrice: 13, image: "/placeholder.svg?height=100&width=100", toppings: [] },
]

export async function GET() {
  return NextResponse.json(pizzas)
}

