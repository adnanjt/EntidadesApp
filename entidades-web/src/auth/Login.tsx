import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login } from "../services/auth";
import { TextField, Button, Container, Typography } from "@mui/material";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const token = await login(username, password);
    if (token) {
      localStorage.setItem("token", token);
      navigate("/");
    } else {
      alert("Credenciales inválidas");
    }
  };

  return (
    <Container maxWidth="sm">
      <Typography variant="h4" gutterBottom>Iniciar Sesión</Typography>
      <form onSubmit={handleSubmit}>
        <TextField fullWidth label="Usuario" margin="normal" value={username} onChange={e => setUsername(e.target.value)} />
        <TextField fullWidth label="Contraseña" type="password" margin="normal" value={password} onChange={e => setPassword(e.target.value)} />
        <Button fullWidth type="submit" variant="contained">Entrar</Button>
      </form>
    </Container>
  );
}
