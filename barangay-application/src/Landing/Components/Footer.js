import React from 'react';
import { Box, Stack } from '@mui/system';
import './Footer.css';
import phlogo from '../Images/PhLogo.png';
import seal from '../Images/CebuCitySeal.png';

const Footer = () => {
    return (
        <Box className="Footer">
            <Stack direction="row" gap="7em">
                <img
                    src={phlogo}
                    alt="PhLogo"
                    className="Footer-Logo"
                />
                <Stack direction="column">
                    <h5 className="FText1">CONTACT US</h5>
                    <h5 className="FText2">Trunkline: (+632) 411 0100</h5>
                </Stack>
                <Stack direction="column">
                    <h5 className="FText1">EMAIL</h5>
                    <h5 className="FText2">cityadmin@cebucity.gov.ph</h5>
                </Stack>
                <Stack direction="column">
                    <h5 className="FText1">OFFICE HOURS</h5>
                    <h5 className="FText2">8:00 AM – 5:00 PM (Monday to Friday)</h5>
                </Stack>
                <img
                    src={seal}
                    alt="Seal"
                    className="Footer-Logo"
                />
            </Stack>
        </Box>
    );
};

export default Footer;