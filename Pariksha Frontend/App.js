import React, { useState } from 'react';  // Import useState hook
import './App.css';
import UserRegistration from './Componenets/UserRegistration';

function App() {
  // Define the state 'isUser' and the setter 'setIsUser'
  const [isUser, setIsUser] = useState(true); // Default to User Registration

  return (
    <div>
      <h1>Registration</h1>
      <div>
        <button onClick={() => setIsUser(true)}>User Registration</button>
        {/* You can add more buttons if needed */}
      </div>

      {/* Conditionally render the UserRegistration component */}
      {isUser && <UserRegistration />}
    </div>
  );
}

export default App;
