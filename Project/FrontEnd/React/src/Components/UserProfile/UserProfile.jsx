import React, { useState } from 'react';
import { FaUserCircle, FaTrashAlt, FaUpload } from 'react-icons/fa';
import { IoIosNotifications } from 'react-icons/io';

const UserProfile = () => {
  const [photo, setPhoto] = useState(null);
  const [notifications, setNotifications] = useState([
    'Welcome to your profile!',
    'Your role was updated recently.',
    'New permission added: can_manage_users',
  ]);

  const user = {
    name: 'Santhosh Kumar',
    userId: 'USR12345',
    gender: 'Male',
    address: 'No. 42, Gandhi Street, Sholinghur, Tamil Nadu',
    role: 'Admin',
    permissions: ['create_user', 'read_user', 'delete_user', 'manage_roles'],
  };

  const handlePhotoUpload = (e) => {
    const file = e.target.files[0];
    if (file) {
      setPhoto(URL.createObjectURL(file));
    }
  };

  const handlePhotoDelete = () => {
    setPhoto(null);
  };

  return (
    <div className="container py-4">
      <div className="card shadow border-0 rounded-4 mb-4">
        <div className="card-header bg-primary text-white rounded-top-4">
          <h4 className="mb-0 d-flex align-items-center gap-2">
            <FaUserCircle /> User Profile
          </h4>
        </div>
        <div className="card-body d-flex flex-column flex-md-row gap-4">
          {/* Profile Photo */}
          <div className="text-center">
            {photo ? (
              <img
                src={photo}
                alt="User"
                className="rounded-circle border border-2"
                style={{ width: '150px', height: '150px', objectFit: 'cover' }}
              />
            ) : (
              <FaUserCircle size={150} className="text-secondary" />
            )}
            <div className="mt-3">
              <label className="btn btn-sm btn-outline-primary me-2">
                <FaUpload className="me-1" />
                Upload
                <input
                  type="file"
                  hidden
                  accept="image/*"
                  onChange={handlePhotoUpload}
                />
              </label>
              {photo && (
                <button
                  className="btn btn-sm btn-outline-danger"
                  onClick={handlePhotoDelete}
                >
                  <FaTrashAlt className="me-1" />
                  Delete
                </button>
              )}
            </div>
          </div>

          {/* User Details */}
          <div className="w-100">
            <div className="row mb-2">
              <div className="col-md-6">
                <strong>Name:</strong>
                <p className="text-muted">{user.name}</p>
              </div>
              <div className="col-md-6">
                <strong>User ID:</strong>
                <p className="text-muted">{user.userId}</p>
              </div>
              <div className="col-md-6">
                <strong>Gender:</strong>
                <p className="text-muted">{user.gender}</p>
              </div>
              <div className="col-md-6">
                <strong>Address:</strong>
                <p className="text-muted">{user.address}</p>
              </div>
              <div className="col-md-6">
                <strong>Role:</strong>
                <p className="badge bg-info text-dark">{user.role}</p>
              </div>
              <div className="col-md-6">
                <strong>Permissions:</strong>
                <div className="d-flex flex-wrap gap-2 mt-1">
                  {user.permissions.map((perm, i) => (
                    <span key={i} className="badge bg-secondary text-light">
                      {perm}
                    </span>
                  ))}
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      {/* Notifications */}
      <div className="card shadow border-0 rounded-4">
        <div className="card-header bg-warning text-dark rounded-top-4 d-flex align-items-center">
          <IoIosNotifications size={24} className="me-2" />
          <h5 className="mb-0">Notifications</h5>
        </div>
        <div className="card-body">
          {notifications.length === 0 ? (
            <p className="text-muted">No notifications yet.</p>
          ) : (
            <ul className="list-group list-group-flush">
              {notifications.map((note, index) => (
                <li key={index} className="list-group-item">
                  {note}
                </li>
              ))}
            </ul>
          )}
        </div>
      </div>
    </div>
  );
};

export default UserProfile;
