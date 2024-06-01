import React, { useState } from 'react';
import Header from './Header';
import BarangayList from './BarangayList';
import Stack from '@mui/material/Stack';
import Map from './Map';
import { useNavigate } from 'react-router-dom';
import ArrowCircleRightIcon from '@mui/icons-material/ArrowCircleRight';
import IconButton from '@mui/material/IconButton';

function MapScreen() {
  const [selectedBarangay, setSelectedBarangay] = useState(null);
  const [name, setName] = useState("");
  const navigate = useNavigate();

  const handleName = (name) => {
    setName(name);
  }

  const handleButton = () => {
    navigate(`/barangays/${name}/families`);
  }

  return (
    <div 
    style={{
        width: "100vw",
        height: "100vh"
    }}>
      <Header/>
      <Stack direction="row" justifyContent="space-evenly" alignItems="flex-start" marginTop="100px">
        <BarangayList setSelectedBarangay={setSelectedBarangay} nameHandler={handleName}/>
        <Map selectedBarangay={selectedBarangay}/>
      </Stack>
      {selectedBarangay && ( // if a barangay is selected, show the pinImage
        <IconButton onClick={handleButton}>
          <ArrowCircleRightIcon 
            sx={{
              color: "#BA0018",
              width: "100px",
              height: "100px",
              position: "absolute",
              left: 1350,
              bottom: 150
            }}
          />
        </IconButton>
      )}
    </div>
  )
}

export default MapScreen