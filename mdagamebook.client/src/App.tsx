import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { AuthProvider } from './contexts/AuthContext';
// pages
import StartingPage from './app/StartingPage';
import Login from './app/Login';
import Upload from './app/Upload';
import Scene from './app/Scene';

// First, create the routes configuration
const routes = [
  {
    path: "/",
    element: <StartingPage />
  },
  {
    path: "/start",
    element: <StartingPage />
  },
  {
    path: "/login",
    element: <Login />
  },
  {
    path: "/upload",
    element: <Upload />
  },
  {
    path: "/scene/:sceneId",
    element: <Scene />
  },
  {
    path: "*",
    element: <div>404 Not Found</div>
  }
];

// Create a router with authentication wrapper
const router = createBrowserRouter(routes.map(route => ({
  ...route,
  element: (
    <AuthProvider>
      {route.element}
    </AuthProvider>
  )
})));

const App = () => {
  return <RouterProvider router={router} />;
}

export default App;
