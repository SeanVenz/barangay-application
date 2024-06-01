import React, { useState } from 'react';
import { BsFillTrashFill, BsFillPencilFill } from "react-icons/bs";
import { IoIosEye } from "react-icons/io";
import "./Table.css";
import { useNavigate, useParams } from 'react-router-dom';

const Table = ({ rows, deleteRow, editRow }) => {
    const navigate = useNavigate();
    const params = useParams();
    const [name, setName] = useState(params.name);
  
    const handleRowClick = (id) => {
      navigate(`/barangays/${name}/families/${id}/members`);
    };
  
    return (
      <div className="table-wrapper">
        <table className="table">
          <thead>
            <tr>
              <th>Registration ID</th>
              <th>Family Name</th>
              <th>Sitio</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {Array.isArray(rows) &&
              rows.map((row, idx) => (
                <tr key={idx}>
                  <td>{row.id}</td>
                  <td>{row.name}</td>
                  <td>{row.sitio}</td>
                  <td>
                    <span className="actions">
                      <IoIosEye
                        className="view-btn"
                        onClick={() => handleRowClick(row.id)}
                      />
                      <BsFillPencilFill
                        className="edit-btn"
                        onClick={() => editRow(idx)}
                      />
                      <BsFillTrashFill
                        className="delete-btn"
                        onClick={() => deleteRow(idx)}
                      />
                    </span>
                  </td>
                </tr>
              ))}
          </tbody>
        </table>
      </div>
    );
  };
  
  export default Table;
  