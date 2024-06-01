import Box from '@mui/material/Box';
import Tab from '@mui/material/Tab';
import Tabs from '@mui/material/Tabs';
import logo from '../Images/Logo.png';
import { NavLink } from 'react-router-dom';
import { useState } from 'react';

function Header() {
  const [selectedTab, setSelectedTab] = useState(0);

  const handleTabChange = (event, newValue) => {
    setSelectedTab(newValue);
  };

  return (
    <Box
      sx={{
        width: "100%",
        height: "100px",
        backgroundColor: '#BA0018',
      }}
    >
      <NavLink to="/barangays">
        <img
          src={logo}
          alt="Logo"
          style={{
            width: "70px",
            height: "70px",
            marginTop: "15px",
            marginLeft: "15px",
          }}
        />
      </NavLink>
        <Tabs value={selectedTab} onChange={handleTabChange} indicatorColor="transparent"
        sx={{
          position: "absolute",
          left: "90px",
          top: "25px"
        }}>
          <NavLink to="/barangays">
            <Tab label="View All Barangays" 
            sx={{ 
              fontFamily: "Montserrat Black, sans-serif",
              fontSize: "19px",
              color: "white"
            }}/>
          </NavLink>
          <NavLink to="/">
            <Tab label="Log Out" 
            sx={{ 
              fontFamily: "Montserrat Black, sans-serif",
              color: "white",
              marginLeft: "1065px"
            }}/>
          </NavLink>
        </Tabs>
    </Box>
  )
}

export default Header;