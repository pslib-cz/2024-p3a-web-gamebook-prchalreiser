import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Link from './components/Link';
// pages
import StartingPage from './app/StartingPage';
import Login from './app/Login';
import Upload from './app/Upload';

const App = () => {
  const router = createBrowserRouter([
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
      path: "*",
      element: <div>404 Not Found</div>
    }
  ])

  return (
    <RouterProvider router={router} />
  )
}

export default App;
