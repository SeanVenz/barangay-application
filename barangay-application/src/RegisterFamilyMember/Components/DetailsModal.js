import React, { useState } from "react";
import "./DetailsModal.css";

const DetailsModal = ({ closeForm, onSubmit, defaultValue }) => {
  const [formState, setFormState] = useState(
    defaultValue || {
    lastName: "",
    firstName: "",
    age: "",
    maritalStatus: "",
    birthDate: "",
    gender: "",
    occupation: "",
    contactNo: "",
    religion: "",
  });

  const [errors, setErrors] = useState("");

  const validateForm = () => {
    const {
      lastName,
      firstName,
      age,
      maritalStatus,
      birthDate,
      gender,
      occupation,
      contactNo,
      religion,
    } = formState;
  
    if (
      lastName &&
      firstName &&
      age &&
      maritalStatus &&
      birthDate &&
      gender
    ) {
      setErrors("");
      return true;
    } else if (!lastName || !firstName || !age || !maritalStatus || !birthDate || !gender) {
      let errorFields = [];
      if (!lastName) errorFields.push("lastName");
      if (!firstName) errorFields.push("firstName");
      if (!age) errorFields.push("age");
      if (!maritalStatus) errorFields.push("maritalStatus");
      if (!birthDate) errorFields.push("birthDate");
      if (!gender) errorFields.push("gender");
  
      setErrors(errorFields.join(", "));
      return false;
    } else {
      setErrors("");
      return true;
    }
  };

  const handleChange = (e) => {
    setFormState({ ...formState, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
      if (!validateForm()) return;
        onSubmit(formState);
        closeForm();
  };
  
  return (
    <div
      className="member-modal-container"
      onClick={(e) => {
        if (e.target.className === "member-modal-container") closeForm();
      }}
    >
      <div className="member-modal">
        <h3 className="member-form-title">Family Member Personal Information</h3>
        <form>
          <div className="member-form-group">
            <label htmlFor="lastName">Last Name</label>
            <input
              name="lastName"
              onChange={handleChange}
              value={formState.lastName}
            />
          </div>
          <div className="member-form-group">
            <label htmlFor="firstName">First Name</label>
            <input
              name="firstName"
              onChange={handleChange}
              value={formState.firstName}
            />
          </div>
          <div className="member-form-group">
            <label htmlFor="age">Age</label>
            <input 
              name="age" 
              onChange={handleChange} 
              value={formState.age} 
            />
          </div>
          <div className="member-form-group">
            <label htmlFor="maritalStatus">Marital Status</label>
            <select
              name="maritalStatus"
              onChange={handleChange}
              value={formState.maritalStatus}
            >
              <option value="N/A"></option>
              <option value="Single">Single</option>
              <option value="Married">Married</option>
              <option value="Widowed">Widowed</option>
              <option value="Separated">Separated</option>
              <option value="Divorced">Divorced</option>
            </select>
          </div>
          <div className="member-form-group">
            <label htmlFor="birthDate">Birthdate</label>
            <input
              type="date"
              name="birthDate"
              onChange={handleChange}
              value={formState.birthDate}
            />
          </div>
          <div className="member-form-group">
            <label htmlFor="gender">Gender</label>
            <select
              name="gender"
              onChange={handleChange}
              value={formState.gender}
            >
              <option value="N/A"></option>
              <option value="Male">Male</option>
              <option value="Female">Female</option>
            </select>
          </div>
          <div className="form-group">
            <label htmlFor="occupation">Occupation</label>
            <input
              name="occupation"
              onChange={handleChange}
              value={formState.occupation || ""}
            />
          </div>
          <div className="form-group">
            <label htmlFor="contactNo">Contact No.</label>
            <input
              name="contactNo"
              onChange={handleChange}
              value={formState.contactNo || ""}
            />
          </div>
          <div className="form-group">
            <label htmlFor="religion">Religion</label>
            <input
              name="religion"
              onChange={handleChange}
              value={formState.religion || ""}
            />
          </div>
          {errors && (
            <div className="error">
              <h4 style={{ marginTop: "none", marginBottom: 5 }}>
                Please input the following required information:
              </h4>
              {errors.split(",").map((error, index) => (
                <div key={index}>{error.toLowerCase()}</div>
              ))}
            </div>
          )}
          <button 
            type="submit" 
            className="btn" 
            onClick={handleSubmit}>
              SUBMIT
          </button>
        </form>
      </div>
    </div>
    );
  };

  export default DetailsModal;