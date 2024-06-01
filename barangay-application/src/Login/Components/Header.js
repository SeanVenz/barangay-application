import React from 'react';
import { Stack } from '@mui/material';
import logo from '../Images/Logo.png';
import "./Header.css";
import { NavLink } from 'react-router-dom';

function Header() {
  return (
    <div className='Login-header-bg'>
      <Stack direction="row">
        <NavLink to="/">
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
          <div style={{width: "370px", height: "100px",textAlign: "left", marginLeft: 20}}>
            <h5 style={{marginTop: 25, marginBottom: 1, fontSize: 16, fontFamily: "Montserrat Black, sans-serif"}}>Republic of the Philippines</h5> 
            <h3 style={{marginTop: 0, fontSize: 22, fontFamily: "Montserrat Black, sans-serif"}}>Barangay Registration System</h3>
          </div>         
      </Stack>
    </div>
  )
}

export default Header;