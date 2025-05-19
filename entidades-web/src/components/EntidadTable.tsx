import React, { useEffect, useState } from "react";
import {
  DataGrid,
  GridColDef,
  GridActionsCellItem,
} from "@mui/x-data-grid";
import { Container, Button, Typography } from "@mui/material";
import { Delete, Edit } from "@mui/icons-material";
import api from "../services/api";
import EntidadForm from "./EntidadForm";

export default function EntidadTable() {
  const [rows, setRows] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);
  const [selected, setSelected] = useState<any | null>(null);
  const [open, setOpen] = useState(false);

  const fetchData = async () => {
    setLoading(true);
    const res = await api.get("/EntidadesGubernamentales");
    setRows(res.data);
    setLoading(false);
  };

  const handleDelete = async (id: string) => {
    await api.delete(`/EntidadesGubernamentales/${id}`);
    fetchData();
  };

  const columns: GridColDef[] = [
    { field: "nombre", headerName: "Nombre", flex: 1 },
    { field: "siglas", headerName: "Siglas", flex: 1 },
    {
      field: "acciones",
      headerName: "Acciones",
      type: "actions",
      getActions: (params) => [
        <GridActionsCellItem icon={<Edit />} label="Editar" onClick={() => {
          setSelected(params.row);
          setOpen(true);
        }} />,
        <GridActionsCellItem icon={<Delete />} label="Eliminar" onClick={() => handleDelete(params.row.id)} />,
      ],
    },
  ];

  useEffect(() => { fetchData(); }, []);

  return (
    <Container>
      <Button variant="contained" onClick={() => { setSelected(null); setOpen(true); }}>Agregar Entidad</Button>
      <div style={{ height: 400, marginTop: 20 }}>
        <DataGrid rows={rows} columns={columns} loading={loading} getRowId={row => row.id} />
      </div>
      <EntidadForm open={open} onClose={() => { setOpen(false); fetchData(); }} entidad={selected} />
    </Container>
  );
}
