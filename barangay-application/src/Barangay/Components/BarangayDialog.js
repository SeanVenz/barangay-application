import * as React from 'react';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogTitle from '@mui/material/DialogTitle';

function BarangayDialog({ open, handleClose, title, handleYes, handleClearInput }) {
    const handleNo = () => {
        handleClose();
      };
    
      const handleYesClick = () => {
        handleYes();
        handleClose();
        handleClearInput();
      };

  return (
    <div>
      <Dialog
        open={open}
        onClose={handleClose}
      >
        <DialogTitle
            sx={{
                fontFamily: "Montserrat Regular, sans-serif",
                color: "#BA0018"
              }}
        >
            {title}
        </DialogTitle>
        <DialogActions>
          <Button onClick={handleNo}
            sx={{
                fontFamily: "Montserrat Regular, sans-serif",
                color: "#BA0018"
              }}
          >
            No
          </Button>
          <Button onClick={handleYesClick} autoFocus
            sx={{
                fontFamily: "Montserrat Regular, sans-serif",
                color: "#BA0018"
              }}
          >
            Yes
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  )
}

export default BarangayDialog