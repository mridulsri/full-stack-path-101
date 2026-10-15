import { Form, type MetaFunction } from "react-router";
import logoDark from "./logo-dark.svg";
import logoLight from "./logo-light.svg";

// export const meta: MetaFunction = () => {
//   return {
//     title: "Welcome",
//     description: "Welcome to our application",
//     "og:image": logoDark,
//     "twitter:image": logoLight,
//   };
// }

export function Welcome() {
  return (
    <div>
      <h1>WelCome</h1>
      <Form method="post">
        <div>
          <input
            type="text"
            name="name"
            id="name"
            autoComplete="false"
            placeholder="Enter your name"
            required
          />
          <input type="submit" value="Submit" />
        </div>
      </Form>
    </div>
  );
}
