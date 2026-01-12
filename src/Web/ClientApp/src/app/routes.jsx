
import NoPage from "@/pages/NoPage";
import Layout from "@/shared/components/layout/Layout";
import LoginPage from "@/features/identity/pages/LoginPage";
import RegisterPage from "@/features/identity/pages/RegisterPage";
import CheckoutPage from "@/features/order/pages/Checkout/CheckoutPage";
import HomePage from "../pages/HomePage";
import { ProductDetailPage } from "@/features/catalog/pages/ProductDetail/ProductDetailPage";
import { BasketPage } from "../features/basket/pages/Basket/BasketPage";
import { OrderResultPage } from "../features/order/pages/OrderResult/OrderResultPage";

export const routes = [
  {
    element: <Layout />,
    children: [
      { path: "/", element: <HomePage /> },
      { path: "/product/:id", element: <ProductDetailPage /> },
    ],
    errorElement: <NoPage />,
  },
  {
    path: "/login",
    element: <LoginPage />,
  },
  {
    path: "/signup",
    element: <RegisterPage />,
  },
  {
    path: "/cart",
    element: <BasketPage />,
  },
  {
    path: "/checkout",
    element: <CheckoutPage />,
  },
  {
    path: "/order/result",
    element: <OrderResultPage/>
  }
];
