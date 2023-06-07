import { Home } from "./components/Home";
import { Measurements } from "./components/Measurements";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/measurements',
    element: <Measurements />
  }
];

export default AppRoutes;
