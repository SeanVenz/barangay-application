import React, { useState, useEffect } from "react";
import { IoMdPersonAdd } from "react-icons/io";
import { Button, Stack } from "@mui/material";
import Form from "./DetailsModal";
import Table from "./Table";
import Header from "./Header";
import "./FamilyMember.css";
import axios from "axios";
import { useParams } from "react-router-dom";
import { ImSearch } from "react-icons/im";

const FamilyMember = () => {
  const params = useParams();
  const [fId] = useState(params.id);
  const [formOpen, setFormOpen] = useState(false);
  const [rows, setRows] = useState([]);
  const [rowToEdit, setRowToEdit] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");

  const fetchData = async () => {
    try {
      const res = await axios.get(`http://localhost:7045/api/families/families-with-members/${fId}`);
      setRows(res.data.familyMembers);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchData();
  }, []);

  const handleDeleteRow = (targetIndex) => {
    const familyMemberId = filteredRows[targetIndex].id;
    if (window.confirm("Are you sure you want to delete this row?")) {
      axios
        .delete(
          `http://localhost:7045/api/members/delete-member-with-details/${familyMemberId}`
        )
        .then((response) => {
          setRows(rows.filter((row) => row.id !== familyMemberId));
          console.log("Family deleted successfully!");
        })
        .catch((error) => {
          console.log(error);
        });
    }
  };

  const handleEditRow = (idx) => {
    const familyMemberId = filteredRows[idx]?.id;
    const prevIndex = rows.findIndex((row) => row.id === familyMemberId);
    setRowToEdit(prevIndex);
    setFormOpen(true);
  };

  const handleSubmit = (newRow) => {
  const apiUrl = rowToEdit !== null
      ? `http://localhost:7045/api/members/${rows[rowToEdit].id}`
      : `http://localhost:7045/api/members/${fId}/family-members-with-details`;

  const httpMethod = rowToEdit !== null ? 'PUT' : 'POST';
    axios({
      method: httpMethod,
      url: apiUrl,
      data: {
          lastName: newRow.lastName,
          firstName: newRow.firstName,
          age: newRow.age,
          maritalStatus: newRow.maritalStatus,
          birthDate: newRow.birthDate,
          gender: newRow.gender,
          occupation: newRow.occupation,
          contactNo: newRow.contactNo,
          religion: newRow.religion,
        }
      })
      .then((response) => {
      if (rowToEdit !== null) {
          const newRows = [...rows];
          newRows[rowToEdit] = response.data;
          setRows(newRows);
      } else {
          const newRows = [...rows, response.data];
          setRows(newRows);
      }
      setFormOpen(false);
      setRowToEdit(null);
      })
      .catch((error) => {
          console.log(error);
      }
    );
  };

  const searchOnChangeHandler = (event) => {
    setSearchTerm(event.target.value);
  };

  const filteredRows = rows.filter((row) => {
    const fullName = `${row.firstName} ${row.lastName}`;
    return fullName.toLowerCase().includes(searchTerm.toLowerCase());
  });

  return (
    <div className="member-page">
      <Header />
      <Stack className="member-search-container" direction="row" justifyContent='center' alignItems='center'>
          <ImSearch className="member-search-icon"/>
          <input
            className="search"
            placeholder="Search Family Member"
            value={searchTerm}
            onChange={searchOnChangeHandler}
          />
      </Stack>
      <Table
        rows={filteredRows}
        editRow={handleEditRow}
        deleteRow={handleDeleteRow} />
      <div className="add-member-btn">
        <Button
          variant="outlined"
          startIcon={<IoMdPersonAdd />}
          onClick={() => {
            setFormOpen(true);
            setRowToEdit(null);
          }}
          style={{
            color: "white",
            backgroundColor: "#BA0018",
            outline: "2px solid #BA0018",
          }}>Add Family Member
        </Button>
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
        </div>
      </div>
    );
  }

export default FamilyMember;