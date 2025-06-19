import { NextResponse } from "next/server"
import type { Topping } from "@/types"

const toppings: Topping[] = [
  { id: 1, name: "Extra Cheese", price: 1 },
  { id: 2, name: "Mushrooms", price: 1.5 },
  { id: 3, name: "Onions", price: 0.5 },
  { id: 4, name: "Sausage", price: 2 },
  { id: 5, name: "Black Olives", price: 1 },
]

export async function GET() {
  return NextResponse.json(toppings)
}

