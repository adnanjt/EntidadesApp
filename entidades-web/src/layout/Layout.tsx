// src/layout/Layout.tsx
import { Sidebar } from "../components/Sidebar";
import { ReactNode } from "react";
import './Layout.css';

interface LayoutProps {
  children: ReactNode;
}

export const Layout = ({ children }: LayoutProps) => {
  return (
    <div className="layout">
      <Sidebar />
      <div className="main-content">
        <header className="header">
          <h2>Nombre de la página</h2>
        </header>
        <div className="page-content">
          {children}
        </div>
      </div>
    </div>
  );
};
