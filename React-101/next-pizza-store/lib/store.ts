import { configureStore } from "@reduxjs/toolkit"
import { pizzaApi } from "@/services/pizzaApi"
import cartReducer from "@/features/cart/cartSlice"

export const store = configureStore({
  reducer: {
    [pizzaApi.reducerPath]: pizzaApi.reducer,
    cart: cartReducer,
  },
  middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(pizzaApi.middleware),
})

export type RootState = ReturnType<typeof store.getState>
export type AppDispatch = typeof store.dispatch

