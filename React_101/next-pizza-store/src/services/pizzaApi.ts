import { createApi, fetchBaseQuery } from "@reduxjs/toolkit/query/react"
import type { Pizza, Topping } from "../types"

export const pizzaApi = createApi({
  reducerPath: "pizzaApi",
  baseQuery: fetchBaseQuery({ baseUrl: "/api" }),
  endpoints: (builder) => ({
    getPizzas: builder.query<Pizza[], void>({
      query: () => "pizzas",
    }),
    getToppings: builder.query<Topping[], void>({
      query: () => "toppings",
    }),
  }),
})

export const { useGetPizzasQuery, useGetToppingsQuery } = pizzaApi

