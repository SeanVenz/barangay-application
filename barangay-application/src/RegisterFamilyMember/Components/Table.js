import React from "react";
import "./Table.css";
import { BsFillTrashFill, BsFillPencilFill } from "react-icons/bs";

const Table = ({ rows, deleteRow, editRow }) => {
  return (
    <div className="member-table-wrapper">
      <table className="member-table">
        <thead>
          <tr>
          <th>Registration ID</th>
            <th>Last Name</th>
            <th>First Name</th>
            <th>Age</th>
            <th>Marital Status</th>
            <th>Birthdate</th>
            <th>Gender</th>
            <th>Occupation</th>
            <th>Contact No.</th>
            <th>Religion</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {rows.map((row, idx) => {
            return (
              <tr key={row.idx}>
                <td>{row.id}</td>
                <td>{row.lastName}</td>
                <td>{row.firstName}</td>
                <td>{row.age}</td>
                <td>{row.maritalStatus}</td>
                <td>{row.birthDate}</td>
                <td>{row.gender}</td>
                <td>{row.occupation}</td>
                <td>{row.contactNo}</td>
                <td>{row.religion}</td>
                <td className="member-fit">
                  <span className="member-actions">
                    <BsFillPencilFill
                      className="member-edit-btn"
                      onClick={() => editRow(idx)}
                    />
                    <BsFillTrashFill
                      className="member-delete-btn"
                      onClick={() => deleteRow(idx)}
                    />
                  </span>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
};

export default Table;