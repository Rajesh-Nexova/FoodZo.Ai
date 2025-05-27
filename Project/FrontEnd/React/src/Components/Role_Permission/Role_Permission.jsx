import React, { useState, useEffect } from 'react';
import { FaEdit, FaTrash, FaPlusCircle } from 'react-icons/fa';
// import axios from 'axios';

const RolePermission = () => {
  const [formData, setFormData] = useState({
    id: '',
    name: '',
    slug: '',
    description: '',
    module: '',
    action: [],
    resource: '',
    is_system_permission: false
  });

  const [permissions, setPermissions] = useState([]);
  const [editingIndex, setEditingIndex] = useState(null);
  const actionsList = ['create', 'read', 'update', 'delete'];
  const modules = ['users', 'posts', 'products', 'orders'];
  const resources = ['user', 'post', 'product', 'order'];

  const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    if (type === 'checkbox' && name === 'is_system_permission') {
      setFormData({ ...formData, [name]: checked });
    } else {
      setFormData({ ...formData, [name]: value });
    }
  };

  const handleActionChange = (e) => {
    const value = e.target.value;
    setFormData(prev => ({
      ...prev,
      action: prev.action.includes(value)
        ? prev.action.filter(a => a !== value)
        : [...prev.action, value]
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (editingIndex !== null) {
      const updated = [...permissions];
      updated[editingIndex] = formData;
      setPermissions(updated);
      setEditingIndex(null);
    } else {
      const newPermission = { ...formData, id: Date.now() };
      setPermissions(prev => [...prev, newPermission]);
    }

    // Simulate storing
    setFormData({
      id: '',
      name: '',
      slug: '',
      description: '',
      module: '',
      action: [],
      resource: '',
      is_system_permission: false
    });

    // Future use:
    // axios.post('/api/permissions', formData)
    //   .then(response => setPermissions([...permissions, response.data]))
    //   .catch(error => console.error(error));
  };

  const handleEdit = (index) => {
    setEditingIndex(index);
    setFormData(permissions[index]);
  };

  const handleDelete = (id) => {
    const updated = permissions.filter(p => p.id !== id);
    setPermissions(updated);

    // axios.delete(`/api/permissions/${id}`)
    //   .then(() => setPermissions(updated))
    //   .catch(error => console.error(error));
  };

  return (
    <div className="container py-5">
      <div className="card shadow border-0">
        <div className="card-header d-flex align-items-center justify-content-between bg-primary text-white">
          <h5 className="mb-0">Role Permission Management</h5>
          <FaPlusCircle />
        </div>
        <div className="card-body">
          <form onSubmit={handleSubmit}>
            <div className="row g-3">
              <div className="col-md-6">
                <label className="form-label">Name</label>
                <input className="form-control" name="name" value={formData.name} onChange={handleChange} required />
              </div>
              <div className="col-md-6">
                <label className="form-label">Slug</label>
                <input className="form-control" name="slug" value={formData.slug} onChange={handleChange} required />
              </div>
              <div className="col-md-12">
                <label className="form-label">Description</label>
                <textarea className="form-control" name="description" value={formData.description} onChange={handleChange} required />
              </div>
              <div className="col-md-4">
                <label className="form-label">Module</label>
                <select className="form-select" name="module" value={formData.module} onChange={handleChange} required>
                  <option value="">Select Module</option>
                  {modules.map((mod, i) => <option key={i} value={mod}>{mod}</option>)}
                </select>
              </div>
              <div className="col-md-4">
                <label className="form-label">Resource</label>
                <select className="form-select" name="resource" value={formData.resource} onChange={handleChange} required>
                  <option value="">Select Resource</option>
                  {resources.map((res, i) => <option key={i} value={res}>{res}</option>)}
                </select>
              </div>
              <div className="col-md-4">
                <label className="form-label">System Permission</label><br />
                <div className="form-check form-switch">
                  <input
                    className="form-check-input"
                    type="checkbox"
                    name="is_system_permission"
                    checked={formData.is_system_permission}
                    onChange={handleChange}
                  />
                  <label className="form-check-label">{formData.is_system_permission ? 'On' : 'Off'}</label>
                </div>
              </div>
              <div className="col-md-12">
                <label className="form-label">Actions</label><br />
                {actionsList.map((act, i) => (
                  <div className="form-check form-check-inline" key={i}>
                    <input
                      className="form-check-input"
                      type="checkbox"
                      id={act}
                      value={act}
                      checked={formData.action.includes(act)}
                      onChange={handleActionChange}
                    />
                    <label className="form-check-label" htmlFor={act}>{act}</label>
                  </div>
                ))}
              </div>
              <div className="col-md-12">
                <button type="submit" className="btn btn-success">{editingIndex !== null ? 'Update' : 'Create'}</button>
              </div>
            </div>
          </form>

          <hr className="my-4" />

          <div className="table-responsive">
            <table className="table table-bordered table-hover align-middle">
              <thead className="table-light">
                <tr>
                  <th>Name</th>
                  <th>Slug</th>
                  <th>Description</th>
                  <th>Module</th>
                  <th>Resource</th>
                  <th>Actions</th>
                  <th>System</th>
                  <th>Manage</th>
                </tr>
              </thead>
              <tbody>
                {permissions.length === 0 ? (
                  <tr><td colSpan="8" className="text-center text-muted">No permissions created yet.</td></tr>
                ) : (
                  permissions.map((perm, index) => (
                    <tr key={perm.id}>
                      <td>{perm.name}</td>
                      <td><code>{perm.slug}</code></td>
                      <td>{perm.description}</td>
                      <td>{perm.module}</td>
                      <td>{perm.resource}</td>
                      <td>
                        {actionsList.map((act) => (
                          <span key={act} className={`badge me-1 bg-${perm.action.includes(act) ? 'primary' : 'secondary'}`}>
                            {act}
                          </span>
                        ))}
                      </td>
                      <td>
                        <span className={`badge bg-${perm.is_system_permission ? 'success' : 'danger'}`}>
                          {perm.is_system_permission ? 'Yes' : 'No'}
                        </span>
                      </td>
                      <td>
                        <button className="btn btn-sm btn-warning me-2" onClick={() => handleEdit(index)}>
                          <FaEdit />
                        </button>
                        <button className="btn btn-sm btn-danger" onClick={() => handleDelete(perm.id)}>
                          <FaTrash />
                        </button>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>

        </div>
      </div>
    </div>
  );
};

export default RolePermission;
