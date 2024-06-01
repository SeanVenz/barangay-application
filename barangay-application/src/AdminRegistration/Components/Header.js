import React from 'react';
import { Box, Stack } from '@mui/system';
import './Header.css';
import logo from '../Images/Logo.png';
import { NavLink } from 'react-router-dom';

const Header = () => {
    return (
        <Box className="Header-container">
            <Stack direction="row">
                <NavLink to="/">
                <img
                    src={logo}
                    alt="Logo"
                    className="Logo"
                />
                </NavLink>
                <div className="Text-container">
                    <h5 className="Text1">Republic of the Philippines</h5> 
                    <h3 className="Text2">Barangay Registration System</h3>
                </div>   
            </Stack>
        </Box>
    );
};

export default Header;