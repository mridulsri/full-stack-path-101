import { Provider } from "react-redux"
import { store } from "./lib/store"
import PizzaStore from "./components/PizzaStore"

function App() {
  return (
    <Provider store={store}>
      <PizzaStore />
    </Provider>
  )
}

export default App

