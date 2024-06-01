import React from 'react';
import { Box, Stack } from '@mui/system';
import './Header.css';
import logo from '../Images/Logo.png';
import { Tab, Tabs } from '@mui/material';
import { NavLink } from 'react-router-dom';

const Header = () => {
    return (
        <Box className="Header-container">
            <Stack direction="row">
                <NavLink to="/barangays">
                <img
                    src={logo}
                    alt="Logo"
                    className="Logo"
                />
                </NavLink>
                <h3 className="viewFamilies">VIEW FAMILY MEMBERS</h3>
                <Tabs style={{marginTop: 25, marginLeft: 10}}>
                    <NavLink to="/">
                    <Tab label="Log Out"  
                        style={{fontFamily: "Montserrat Black, sans-serif", color: "white", marginLeft: "1075px"}}/>
                    </NavLink>
                </Tabs>   
            </Stack>
        </Box>
    );
};

export default Header;