import React, { useEffect, useState } from "react";
import {
  Dialog, 
  DialogTitle, 
  DialogContent, 
  DialogActions,
  Button, 
  TextField, 
  FormControlLabel, 
  Switch,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
  FormHelperText
} from "@mui/material";
import api from "../services/api";
import { SelectChangeEvent } from "@mui/material";


export default function EntidadForm({ open, onClose, entidad }: any) {
  const [form, setForm] = useState({
    nombre: "",
    siglas: "",
    tipoEntidad: "",
    descripcion: "",
    paginaWeb: "",
    telefono: "",
    correoElectronico: "",
    direccion: "",
    dependencia: "",
    sector: "",
    activo: true,
    fechaCreacion: new Date().toISOString()
  });

  useEffect(() => {
    if (entidad) {
      setForm({
        ...entidad,
        fechaCreacion: entidad.fechaCreacion || new Date().toISOString()
      });
    } else {
      setForm({
        nombre: "",
        siglas: "",
        tipoEntidad: "",
        descripcion: "",
        paginaWeb: "",
        telefono: "",
        correoElectronico: "",
        direccion: "",
        dependencia: "",
        sector: "",
        activo: true,
        fechaCreacion: new Date().toISOString()
      });
    }
  }, [entidad]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value, type, checked } = e.target;
    setForm((prev) => ({
      ...prev,
      [name]: type === "checkbox" ? checked : value
    }));
  };

const handleChangeSelect = (event: SelectChangeEvent<string>) => {
  const { name, value } = event.target;
  setForm((prev) => ({
    ...prev,
    [name]: value
  }));
};

  const [errors, setErrors] = useState<{ [key: string]: string }>({});


  const handleSubmit = async () => {
    if (!validateForm()) return;

    try {
      if (entidad?.id) {
        await api.put(`/EntidadesGubernamentales/${entidad.id}`, form);
      } else {
        await api.post("/EntidadesGubernamentales", form);
      }
      onClose();
    } catch (err) {
      console.error("Error al guardar la entidad:", err);
    }
  };


  const validateForm = () => {
    const newErrors: { [key: string]: string } = {};

    if (!form.nombre.trim()) newErrors.nombre = "Nombre es requerido";
    if (!form.siglas.trim()) newErrors.siglas = "Siglas es requerido";
    if (!form.tipoEntidad.trim()) newErrors.tipoEntidad = "Tipo de entidad es requerido";
    if (!form.sector.trim()) newErrors.sector = "Sector es requerido";

    if (form.correoElectronico && !/\S+@\S+\.\S+/.test(form.correoElectronico)) {
      newErrors.correoElectronico = "Correo inválido";
    }

    if (form.paginaWeb && !/^https?:\/\/.+\..+/.test(form.paginaWeb)) {
      newErrors.paginaWeb = "URL inválida";
    }

    if (form.telefono && !/^\+?[0-9\s\-()]{7,}$/.test(form.telefono)) {
      newErrors.telefono = "Teléfono inválido";
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const tipoEntidadOptions = [
  "Gubernamental",
  "No gubernamental",
  "Privada",
  "Sin fines de lucro",
];

const sectorOptions = [
  "Economía",
  "Turismo",
  "Educación",
  "Salud",
  "Tecnología",
  "Transporte",
  "Otros",
];


  return (
    <Dialog open={open} onClose={onClose} maxWidth="md" fullWidth>
      <DialogTitle>{entidad ? "Editar Entidad" : "Agregar Entidad"}</DialogTitle>
      <DialogContent>
        {[
          "nombre",
          "siglas",
          "tipoEntidad",
          "descripcion",
          "paginaWeb",
          "telefono",
          "correoElectronico",
          "direccion",
          "dependencia",
          "sector"
        ].map((field) => {
            const label = field.charAt(0).toUpperCase() + field.slice(1);

            switch (field) {
              case "tipoEntidad":
                return (
                  <FormControl key={field} fullWidth margin="normal" error={!!errors[field]}>
                    <InputLabel>{label}</InputLabel>
                    <Select
                      name={field}
                      value={form[field]}
                      onChange={handleChangeSelect}
                      label={label}
                    >
                      {tipoEntidadOptions.map((option) => (
                        <MenuItem key={option} value={option}>
                          {option}
                        </MenuItem>
                      ))}
                    </Select>
                    {errors[field] && <FormHelperText>{errors[field]}</FormHelperText>}
                  </FormControl>
                );
              case "sector":
                return (
                  <FormControl key={field} fullWidth margin="normal" error={!!errors[field]}>
                    <InputLabel>{label}</InputLabel>
                    <Select
                      name={field}
                      value={form[field]}
                      onChange={handleChangeSelect}
                      label={label}
                    >
                      {sectorOptions.map((option) => (
                        <MenuItem key={option} value={option}>
                          {option}
                        </MenuItem>
                      ))}
                    </Select>
                    {errors[field] && <FormHelperText>{errors[field]}</FormHelperText>}
                  </FormControl>
                );
              default:
                return (
                  <TextField
                    key={field}
                    fullWidth
                    margin="normal"
                    label={label}
                    name={field}
                    value={form[field as keyof typeof form]}
                    onChange={handleChange}
                    error={!!errors[field]}
                    helperText={errors[field]}
                  />
                );
          }
        
        })}
        <FormControlLabel
          control={
            <Switch
              checked={form.activo}
              onChange={handleChange}
              name="activo"
              color="primary"
            />
          }
          label="Activo"
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancelar</Button>
        <Button onClick={handleSubmit} variant="contained">Guardar</Button>
      </DialogActions>
    </Dialog>
  );
}
