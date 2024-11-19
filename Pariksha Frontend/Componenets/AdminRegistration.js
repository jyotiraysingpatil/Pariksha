import React, { useState } from 'react';
import axios from 'axios';

const AdminRegistration = () => {
    const [adminDetails, setAdminDetails] = useState({
        firstName: '',
        lastName: '',
        username: '',
        password: '',
        isActive: true
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setAdminDetails({
            ...adminDetails,
            [name]: value
        });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.post('http://localhost:5000/api/admin/register', adminDetails);
            alert('Admin registered successfully');
        } catch (error) {
            alert('Registration failed');
        }
    };

    return (
        <div>
            <h2>Admin Registration</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>First Name:</label>
                    <input type="text" name="firstName" value={adminDetails.firstName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Last Name:</label>
                    <input type="text" name="lastName" value={adminDetails.lastName} onChange={handleChange} required />
                </div>
                <div>
                    <label>Username:</label>
                    <input type="text" name="username" value={adminDetails.username} onChange={handleChange} required />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" name="password" value={adminDetails.password} onChange={handleChange} required />
                </div>
                <div>
                    <label>Is Active:</label>
                    <input type="checkbox" name="isActive" checked={adminDetails.isActive} onChange={(e) => setAdminDetails({ ...adminDetails, isActive: e.target.checked })} />
                </div>
                <button type="submit">Register</button>
            </form>
        </div>
    );
};

export default AdminRegistration;
