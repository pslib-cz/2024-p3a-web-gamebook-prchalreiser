import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import { AuthProvider } from './contexts/AuthContext';
import { SceneProvider } from './contexts/SceneContext';
// pages
import StartingPage from './pages/StartingPage';
import Login from './pages/Login';
import Scene from './pages/Scene';
import AdminPanel from './pages/admin/AdminPanel';

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
    path: "/scene/:sceneId",
    element: <Scene />
  },
  {
    path: "/admin",
    element: <AdminPanel />
  },
  {
    path: "*",
    element: <div>404 Not Found</div>
  }
];

const router = createBrowserRouter(routes.map(route => ({
  ...route,
  element: (
    <AuthProvider>
      <SceneProvider>
        {route.element}
      </SceneProvider>
    </AuthProvider>
  )
})), {
  basename: import.meta.env.BASE_URL || '/'
});

const App = () => {
  return <RouterProvider router={router} />;
}

export default App;
