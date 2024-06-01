import React, { useEffect, useState } from 'react';
import Header from './Header';
import { Box, Stack } from '@mui/material';
import './Family.css';
import { ImUndo2, ImPlus } from "react-icons/im";
import Table from './Table';
import Form from "./AddFamily";
import axios from 'axios';
import { useParams, useNavigate } from 'react-router-dom';
import { ImSearch } from "react-icons/im";

const Family = () => {
    const params = useParams();
    const navigate = useNavigate();
    const [bId, setBId] = useState(0);
    const [barangay, setBarangay] = useState(params.name.toUpperCase());
    const [searchTerm, setSearchTerm] = useState("");

    const [formOpen, setFormOpen] = useState(false);

    const [rows, setRows] = useState([]);

    const [rowToEdit, setRowToEdit] = useState(null);

    const backButtonHandler = () => {
        navigate(-1);
    }

    useEffect(() => {
        const fetchData = async () => {
          try {
            const response1 = await axios.get(`http://localhost:7045/api/barangays/search/${barangay}`);
            setBId(response1.data.id);

            const response2 = await axios.get(`http://localhost:7045/api/barangays/${response1.data.id}/families`);
            setRows(response2.data);
          } catch (error) {
            console.error(error);
          }
        };

        fetchData();
      }, [formOpen]);

      const handleDeleteRow = (targetIndex) => {
        if (rows && rows.length > 0) {
          const familyId = filteredRows[targetIndex].id;
          if (window.confirm("Are you sure you want to delete this row?")) {
            axios
              .delete(`http://localhost:7045/api/families/${familyId}/delete`)
              .then((response) => {
                const updatedRows = rows.filter((row) => row.id !== familyId);
                setRows(updatedRows);
                console.log("Family deleted successfully!");
              })
              .catch((error) => {
                console.log(error);
              });
          }
        }
      };

    const handleEditRow = (idx) => {
        const familyId = filteredRows[idx]?.id;
        const originalIndex = rows.findIndex((row) => row.id === familyId);
        setRowToEdit(originalIndex);
        setFormOpen(true);
    };

    const handleSubmit = (newRow) => {
      const apiUrl =
        rowToEdit !== null
          ? `http://localhost:7045/api/families/${rows[rowToEdit].id}/update`
          : `http://localhost:7045/api/barangays/${bId}/family/register`;

      const httpMethod = rowToEdit !== null ? 'PUT' : 'POST';

      axios({
        method: httpMethod,
        url: apiUrl,
        data: {
          name: newRow.name,
          sitio: newRow.sitio,
        },
      })
        .then(() => {
          // Fetch the updated data from the API after successful POST/PUT request
          axios.get(`http://localhost:7045/api/barangays/${bId}/families`)
            .then((response) => {
              setRows(response.data); // Update the state with the fetched data
              setFormOpen(false);
              setRowToEdit(null);
            })
            .catch((error) => {
              console.log(error);
            });
        })
        .catch((error) => {
          console.log(error);
        });
    };

    const searchOnChangeHandler = (event) => {
      setSearchTerm(event.target.value);
    };

    const filteredRows = rows && rows.length > 0
    ? rows.filter((row) => {
        const search = `${row.name} ${row.sitio}`;
        return search.toLowerCase().includes(searchTerm.toLowerCase());
      })
    : [];


    return (
        <div className="MainContainer">
            <Header />
            <Stack direction="row" marginLeft="365px" marginTop="30px">
                <Box className="BLabelContainer">
                    <h4 className="h4Text1">BARANGAY:</h4>
                </Box>
                <Box className="BNameContainer">
                    <h4 className="h4Text2">{barangay}</h4>
                </Box>
            </Stack>
            <Stack direction="column" marginTop="50px">
                <Box className="FunctionContainer">
                    <Stack direction="row" gap="140px">
                        <button className="back-button" onClick={backButtonHandler}>
                            <ImUndo2 className="back-icon"/>
                        </button>
                        <div className="search-container">
                            <Stack direction="row">
                                <ImSearch className="search-icon"/>
                                <input
                                    className="search"
                                    placeholder="Search Family"
                                    value={searchTerm}
                                    onChange={searchOnChangeHandler}
                                />
                            </Stack>
                        </div>
                        <button className="add-button">
                            <ImPlus className="add-icon" onClick={() => setFormOpen(true)}/>
                        </button>
                        {formOpen && (
                            <Form
                                closeForm={() => {
                                    setFormOpen(false);
                                    setRowToEdit(null);
                                }}
                                onSubmit={handleSubmit}
                                defaultValue={rowToEdit !== null && rows[rowToEdit]}
                            />
                        )}
                    </Stack>
                </Box>
                <Table rows={filteredRows} editRow={handleEditRow} deleteRow={handleDeleteRow} />
            </Stack>
        </div>
    );
};

export default Family;