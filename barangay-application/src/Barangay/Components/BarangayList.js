import React, { useState, useEffect } from 'react';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography';
import Accordion from '@mui/material/Accordion';
import AccordionSummary from '@mui/material/AccordionSummary';
import AccordionDetails from '@mui/material/AccordionDetails';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import axios from 'axios';

function BarangayList({ setSelectedBarangay, nameHandler }) {
  const [barangays, setBarangays] = useState([]);

  useEffect(() => {
    axios.get('http://localhost:7045/api/barangays')
      .then(response => {
        setBarangays(response.data);
        response.data.forEach((barangay, index) => {
          axios.get(`http://localhost:7045/api/barangays/population/${barangay.id}`)
            .then(response => {
              setBarangays(prevState => {
                prevState[index].population = response.data;
                return [...prevState];
              });
            })
            .catch(error => {
              console.log(error);
            });
        });
      })
      .catch(error => {
        console.log(error);
      });
  }, []);

  const handleAccordionChange = (barangay) => (event, isExpanded) => {
    if (isExpanded) {
      setSelectedBarangay(barangay);
      nameHandler(barangay.name.toLowerCase());
    } else {
      setSelectedBarangay(null);
    }
  };

  return (
    <Stack
      direction="column"
      alignItems="center"
      sx={{
        height: "400px",
        overflow: "auto"
      }}
    >
      <Typography variant="h2" fontFamily="Montserrat Black, sans-serif" color="#BA0018">
        BARANGAYS
      </Typography>
      {barangays && barangays.map((barangay) => (
        <Accordion
          key={barangay.id}
          onChange={handleAccordionChange(barangay)}
          sx={{
            width: "400px",
            border: "solid 5px #BA0018"
          }}
        >
          <AccordionSummary
            expandIcon={<ExpandMoreIcon
              sx={{
                color: "#BA0018"
              }}
            />}
          >
            <Typography variant="h5" fontFamily="Montserrat Black, sans-serif" color="#BA0018">
              {barangay.name && barangay.name.toUpperCase()}
            </Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography variant="body1" fontFamily="Montserrat Regular, sans-serif" color="#BA0018">
              Captain: {barangay.captain}
              <br />
              Population: {barangay.population}
            </Typography>
          </AccordionDetails>
        </Accordion>
      ))}
    </Stack>
  );
}

export default BarangayList;
