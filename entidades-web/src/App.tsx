import React from "react";
import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "./auth/Login";
import EntidadTable from "./components/EntidadTable";
import PrivateRoute from "./auth/PrivateRoute";
import { Layout } from "./layout/Layout";

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* Login sin Layout */}
        <Route path="/login" element={<Login />} />

        {/* Rutas protegidas con Layout */}
        <Route element={<PrivateRoute />}>
          <Route
            path="/"
            element={
              <Layout>
                <EntidadTable />
              </Layout>
            }
          />
        </Route>

        {/* Ruta por defecto */}
        <Route path="*" element={<Navigate to="/" />} />
      </Routes>
    </BrowserRouter>
  );
}
