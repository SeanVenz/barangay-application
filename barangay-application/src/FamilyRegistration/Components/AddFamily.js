import React, { useState } from 'react';
import "./AddFamily.css";

const AddFamily = ({ closeForm, onSubmit, defaultValue }) => {
    const [formState, setFormState] = useState(
        defaultValue || {
          name: "",
          sitio: "",
        }
    );
    
    const [errors, setErrors] = useState("");

    const validateForm = () => {
        if (formState.name &&
            formState.sitio) {
            setErrors("");
            return true;
        } else {
            let errorFields = [];
            for (const [key, value] of Object.entries(formState)) {
                if (!value) {
                    errorFields.push(key);
                }
            }
            setErrors(errorFields.join(", "));
            return false;
        }
    };

    const handleChange = (e) => {
        setFormState({ ...formState, [e.target.name]: e.target.value });
    };
    
    const handleSubmit = (e) => {
        e.preventDefault();
        if (!validateForm()) return;
        onSubmit(formState);
        closeForm();
    };

    return (
        <div
            className="modal-container"
            onClick={(e) => {
                if (e.target.className === "modal-container") closeForm();
            }}
        >
            <div className="modal">
                <h3 className="form-title">Family Information</h3>
                <form>
                    <div className="form-group">
                        <label htmlFor="name">Family Name</label>
                        <input 
                            name="name" 
                            onChange={handleChange} 
                            value={formState.name} 
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="sitio">Sitio</label>
                        <input
                            name="sitio"
                            onChange={handleChange}
                            value={formState.sitio}
                        />
                    </div>
                    {errors && (
                        <div className="error">
                            <h4 style={{ marginTop: "none", marginBottom: 5 }}>Please input the following required information:</h4>
                            {errors.split(',').map((error, index) => (
                                <div key={index}>{error}</div>
                            ))}
                        </div>
                    )}
                    <button type="submit" className="btn" onClick={handleSubmit}>
                        SUBMIT
                    </button>
                </form>
            </div>
        </div>
    );
};

export default AddFamily;