import React, { useState, useEffect } from 'react';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import axios from 'axios';
import BarangayDialog from './BarangayDialog';

function RegisterUpdateDeleteBarangayForm() {
  const [barangays, setBarangays] = useState([]);
  const [selectedBarangay, setSelectedBarangay] = useState([]);
  const [inputBarangay, setInputBarangay] = useState("");
  const [inputCaptain, setInputCaptain] = useState("");
  const [openRegister, setOpenRegister] = useState(false);
  const [openUpdate, setOpenUpdate] = useState(false);
  const [openDelete, setOpenDelete] = useState(false);
  const [mode, setMode] = useState("");
  const title1 = `Are you sure to register Barangay ${inputBarangay}?`;
  const title2 = `Are you sure to update Barangay ${inputBarangay ? inputBarangay : selectedBarangay}?`;
  const title3 = `Are you sure to delete Barangay ${inputBarangay ? inputBarangay : selectedBarangay}?`;

  const handleSelectedBarangay = (event) => {
    setSelectedBarangay(event.target.value);
  };
  const handleInputBarangay = (event) => {
    setInputBarangay(event.target.value);
  }
  const handleInputCaptain = (event) => {
    setInputCaptain(event.target.value);
  }
  const handleClearInput = (event) => {
    setInputBarangay("");
    setInputCaptain("");
    setSelectedBarangay([]);
  }

  const handleClickOpenRegister = () => {
    setOpenRegister(true);
  };

  const handleCloseRegister = () => {
    setOpenRegister(false);
  };

  const handleClickOpenUpdate = () => {
    setOpenUpdate(true);
  };

  const handleCloseUpdate = () => {
    setOpenUpdate(false);
  };

  const handleClickOpenDelete = () => {
    setOpenDelete(true);
  };

  const handleCloseDelete = () => {
    setOpenDelete(false);
  };

  const handleDialogYes = () => {
    switch (mode) {
      case 'create':
        handlePost();
        break;
      case 'update':
        handleUpdate();
        break;
      case 'delete':
        handleDelete();
        break;
      default:
        break;
    }
  };

  const handlePost = () => {
    axios.post('http://localhost:7045/api/barangays', {
      name: inputBarangay,
      captain: inputCaptain,
    })
      .then((response) => {
        console.log("Barangay registered successfully!");
      })
      .catch((error) => {
        console.error(error);
      });
  };
  const handleUpdate = () => {
    const name = inputBarangay === "" ? selectedBarangay : inputBarangay;

    axios.put(`http://localhost:7045/api/barangays/${selectedBarangay}/update`, {
      name: name,
      captain: inputCaptain,
    })
      .then((response) => {
        console.log("Barangay updated successfully!");
      })
      .catch((error) => {
        console.error(error);
      });
  };
  const handleDelete = () => {
    axios.delete(`http://localhost:7045/api/barangays/${selectedBarangay}/delete`)
      .then(response => {
        console.log("Barangay deleted successfully!");
      })
      .catch(error => {
        console.error(error);
      });
  };
  useEffect(() => {
    axios.get('http://localhost:7045/api/barangays?fields=name')
      .then(response => {
        setBarangays(response.data);
      })
      .catch(error => {
        console.error(error);
      });
  }, []);

  return (
    <Stack direction="column" alignItems="center"
      sx={{
        width: "540px",
        height: "430px",
        borderRadius: "10px",
        backgroundColor: '#BA0018',
      }}
    >
      <Typography variant="h5" fontFamily="Montserrat Black, sans-serif" color="white" marginTop="10px">
        SELECT A BARANGAY:
      </Typography>
      {barangays && (
        <Select onChange={handleSelectedBarangay} value={selectedBarangay}
          sx={{
            backgroundColor: "white",
            borderRadius: "10px",
            border: "none",
            marginTop: "10px",
            width: "500px",
            "&:hover .MuiOutlinedInput-notchedOutline": {
              border: "none",
            },
            "&:focus-within .MuiOutlinedInput-notchedOutline": {
              border: "none",
              borderColor: "white"
            },
            ".MuiOutlinedInput-notchedOutline": {
              borderColor: "transparent",
            }
          }}
        >
          {barangays.map(barangay => (
            <MenuItem key={barangay.id} value={barangay.name}>{barangay.name}</MenuItem>
          ))}
        </Select>
      )}
      <Typography variant="h5" fontFamily="Montserrat Black, sans-serif" color="white" marginTop="10px">
        NEW BARANGAY:
      </Typography>
      <TextField variant="outlined" value={inputBarangay} onChange={handleInputBarangay}
        sx={{
          backgroundColor: "white",
          borderRadius: "10px",
          border: "none",
          marginTop: "10px",
          width: "500px",
          "&:hover .MuiOutlinedInput-notchedOutline": {
            border: "none",
          },
          "&:focus-within .MuiOutlinedInput-notchedOutline": {
            border: "none"
          },
          ".MuiOutlinedInput-notchedOutline": {
            borderColor: "transparent",
          }
        }}
      />
      <Typography variant="h5" fontFamily="Montserrat Black, sans-serif" color="white" marginTop="10px">
        NEW CAPTAIN:
      </Typography>
      <TextField variant="outlined" value={inputCaptain} onChange={handleInputCaptain}
        sx={{
          backgroundColor: "white",
          borderRadius: "10px",
          border: "none",
          marginTop: "10px",
          width: "500px",
          "&:hover .MuiOutlinedInput-notchedOutline": {
            border: "none",
          },
          "&:focus-within .MuiOutlinedInput-notchedOutline": {
            border: "none"
          },
          ".MuiOutlinedInput-notchedOutline": {
            borderColor: "transparent",
          }
        }}
      />
      <Stack direction="row" alignItems="center" marginTop="50px" spacing={1}>
        <Button variant="contained" onClick={() => { handleClickOpenRegister(); setMode("create"); }}
          sx={{
            backgroundColor: "white",
            width: "100px",
            "&:hover": {
              backgroundColor: "#BA0018",
              "& .MuiTypography-root": {
                color: "white",
              },
            },
          }}
        >
          <Typography variant="body1" fontFamily="Montserrat Regular, sans-serif" color="#BA0018">
            REGISTER
          </Typography>
        </Button>
        <BarangayDialog open={openRegister} handleClose={handleCloseRegister} title={title1} handleYes={handleDialogYes} handleClearInput={handleClearInput} />
        <Button variant="contained" onClick={() => { handleClickOpenUpdate(); setMode("update"); }}
          sx={{
            backgroundColor: "white",
            width: "100px",
            "&:hover": {
              backgroundColor: "white",
              "&:hover": {
                backgroundColor: "#BA0018",
                "& .MuiTypography-root": {
                  color: "white",
                },
              },
            }
          }}
        >
          <Typography variant="body1" fontFamily="Montserrat Regular, sans-serif" color="#BA0018">
            UPDATE
          </Typography>
        </Button>
        <BarangayDialog open={openUpdate} handleClose={handleCloseUpdate} title={title2} handleYes={handleDialogYes} handleClearInput={handleClearInput} />
        <Button onClick={() => { handleClickOpenDelete(); setMode("delete"); }}
          variant="contained"
          sx={{
            backgroundColor: "white",
            width: "100px",
            "&:hover": {
              backgroundColor: "#BA0018",
              "& .MuiTypography-root": {
                color: "white",
              },
            },
          }}
        >
          <Typography
            variant="body1"
            fontFamily="Montserrat Regular, sans-serif"
            color="#BA0018"
          >
            DELETE
          </Typography>
        </Button>
        <BarangayDialog open={openDelete} handleClose={handleCloseDelete} title={title3} handleYes={handleDialogYes} handleClearInput={handleClearInput} />
      </Stack>
    </Stack>
  )
}

export default RegisterUpdateDeleteBarangayForm;
