import { NavLink } from "react-router-dom";
import './Sidebar.css';
import { FaHome, FaSearch, FaPlus } from 'react-icons/fa';

export const Sidebar = () => {
  return (
    <div className="sidebar">
      <div className="logo">
        <img src="/logo-sb-footer.svg" alt="Logo SB" />
        <h1>Superintendencia<br />de Bancos</h1>
        <p>República Dominicana</p>
      </div>
      <nav className="nav-links">
        <NavLink to="/" end className="nav-link">
          <FaHome className="icon" /> Inicio
        </NavLink>
        <NavLink to="/consulta" className="nav-link">
          <FaSearch className="icon" /> Consulta
        </NavLink>
        <NavLink to="/crear-registro" className="nav-link">
          <FaPlus className="icon" /> Crear registro
        </NavLink>
      </nav>
    </div>
  );
};
