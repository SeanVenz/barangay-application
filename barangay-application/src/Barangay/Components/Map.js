import React from 'react';
import Box from '@mui/material/Box';
import mapImage from '../Images/Map.png';
import pinImage from '../Images/Map Pin.png';

function Map({ selectedBarangay }) {

  return (
    <Box
      sx={{
        width: 405,
        height: 400,
        backgroundImage: `url(${mapImage})`,
        backgroundRepeat: 'no-repeat',
        backgroundPosition: 'center',
        backgroundSize: 'cover',
      }}
    >
        {selectedBarangay && ( // if a barangay is selected, show the pinImage
        <img
          src={pinImage}
          alt="Map Pin"
          style={{
            width: "100px",
            height: "100px",
            marginTop: "100px",
            marginLeft: "200px"
          }}
        />
      )}
    </Box>
  )
}

export default Map