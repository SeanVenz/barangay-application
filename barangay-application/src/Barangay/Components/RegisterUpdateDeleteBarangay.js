import React from 'react';
import UpdateDeleteBarangayHeader from './RegisterUpdateDeleteBarangayHeader';
import UpdateDeleteBarangayForm from './RegisterUpdateDeleteBarangayForm';
import Stack from '@mui/material/Stack';

function RegisterUpdateDeleteBarangay() {
  return (
    <div 
    style={{
        width: "100vw",
        height: "100vh"
    }}>
        <UpdateDeleteBarangayHeader/>
        <Stack direction="row" justifyContent="center" alignItems="center" marginTop="100px">
            <UpdateDeleteBarangayForm/>
        </Stack>
    </div>
  )
}

export default RegisterUpdateDeleteBarangay