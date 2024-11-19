import React, { useState } from 'react';
import axios from 'axios';

const UserRegistration = () => {
    const [userDetails, setUserDetails] = useState({
        firstName: '',
        lastName: '',
        username: '',
        password: '',
        phoneNumber: ''
    });
    const [errorMessage, setErrorMessage] = useState('');

    // Handle input field changes
    const handleChange = (e) => {
        const { name, value } = e.target;
        setUserDetails({
            ...userDetails,
            [name]: value
        });
    };

    // Validate phone number (basic validation)
    const validatePhoneNumber = (phoneNumber) => {
        const phoneRegex = /^[0-9]{10}$/; // Adjust the regex to match your phone number format
        return phoneRegex.test(phoneNumber);
    };

    // Handle form submission
    const handleSubmit = async (e) => {
        e.preventDefault();

        // Validate phone number
        if (!validatePhoneNumber(userDetails.phoneNumber)) {
            setErrorMessage('Invalid phone number format. Please enter a 10-digit number.');
            return;
        }

        try {
            const response = await axios.post('https://localhost:5001/api/users/register', userDetails);
            alert('User registered successfully');
        } catch (error) {
            setErrorMessage(error.response?.data?.message || 'Registration failed. Please try again.');
        }
    };

    return (
        <div>
            <h2>User Registration</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>First Name:</label>
                    <input type="text" name="firstName" value={userDetails.firstName} onChange={handleChange}  />
                </div>
                <div>
                    <label>Last Name:</label>
                    <input type="text" name="lastName" value={userDetails.lastName} onChange={handleChange} />
                </div>
                <div>
                    <label>Username:</label>
                    <input type="text" name="username" value={userDetails.username} onChange={handleChange} />
                </div>
                <div>
                    <label>Password:</label>
                    <input type="password" name="password" value={userDetails.password} onChange={handleChange}  />
                </div>
                <div>
                    <label>Phone Number:</label>
                    <input type="text" name="phoneNumber" value={userDetails.phoneNumber} onChange={handleChange} />
                </div>
                {errorMessage && <div style={{ color: 'red' }}>{errorMessage}</div>}
                <button type="submit">Register</button>
            </form>
        </div>
    );
};

export default UserRegistration;
