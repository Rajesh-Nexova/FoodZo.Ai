import React, { useState } from 'react';
// import axios from 'axios';
import { FaKey } from 'react-icons/fa';

const CreatePermission = () => {
  const [formData, setFormData] = useState({
    name: '',
    slug: '',
    description: '',
    module: '',
    action: [],
    resource: '',
  });

  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');
  const [localPermissions, setLocalPermissions] = useState([]);
  const [editMode, setEditMode] = useState(false);
const [editingIndex, setEditingIndex] = useState(null);


  const modules = ['users', 'posts', 'products', 'orders']; // example modules
  const resources = ['user', 'post', 'product', 'order'];   // example resources
  const actions = ['create', 'read', 'update', 'delete'];

  const handleChange = (e) => {
    setFormData(prev => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
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

  const handleEditPermission = (index) => {
  const selected = localPermissions[index];
  setFormData({
    name: selected.name,
    slug: selected.slug,
    action: selected.action,
    module: selected.module,
    resource: selected.resource,
  });
  setEditMode(true);
  setEditingIndex(index);
};

const handleDeletePermission = async (slug) => {
  try {
    // Call your API to delete from DB
    await fetch(`/api/permissions/${slug}`, {
      method: 'DELETE',
    });

    // Remove from local state
    setLocalPermissions(prev => prev.filter(p => p.slug !== slug));
  } catch (error) {
    console.error('Delete failed:', error);
  }
};
const handleFormSubmit = async (e) => {
  e.preventDefault();

  if (editMode) {
    // UPDATE in database
    await fetch(`/api/permissions/${formData.slug}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(formData),
    });

    // Update local state
    const updated = [...localPermissions];
    updated[editingIndex] = formData;
    setLocalPermissions(updated);

    setEditMode(false);
    setEditingIndex(null);
  } else {
    // CREATE in database
    await fetch('/api/permissions', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(formData),
    });

    // Add to local state
    setLocalPermissions([...localPermissions, formData]);
  }

  setFormData({
    name: '',
    slug: '',
    action: '',
    module: '',
    resource: '',
  });
};



  const validateForm = () => {
    if (!formData.name || !formData.slug || !formData.description || !formData.module || !formData.resource || formData.action.length === 0) {
      return 'All fields are required and at least one action must be selected.';
    }
    return '';
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    setError('');
    setSuccess('');

    const validationMsg = validateForm();
    if (validationMsg) {
      setError(validationMsg);
      return;
    }

    const newPermissions = formData.action.map(act => ({
      name: formData.name,
      slug: formData.slug,
      description: formData.description,
      module: formData.module,
      action: act,
      resource: formData.resource,
      is_system_permission: false
    }));

    // Simulated local storage for now
    setLocalPermissions(prev => [...prev, ...newPermissions]);
    setSuccess('Permissions created successfully!');
    setFormData({
      name: '',
      slug: '',
      description: '',
      module: '',
      action: [],
      resource: '',
    });

    // Future API POST
    /*
    axios.post('/api/permissions', newPermissions)
      .then(res => {
        setSuccess('Permissions created successfully!');
        setFormData({ name: '', slug: '', description: '', module: '', action: [], resource: '' });
      })
      .catch(err => {
        setError('Error creating permission.');
        console.error(err);
      });
    */
  };

  return (
    <div className="container py-5">
      <div className="card shadow border-0">
        <div className="card-header bg-gradient text-white d-flex align-items-center" style={{ background: 'linear-gradient(to right, #00c6ff, #0072ff)' }}>
          <FaKey className="me-2" />
          <h5 className="mb-0 text-dark">Create Permission</h5>
        </div>
        <div className="card-body">
          {error && <div className="alert alert-danger">{error}</div>}
          {success && <div className="alert alert-success">{success}</div>}
          <form onSubmit={handleSubmit}>
            <div className="mb-3">
              <label className="form-label">Permission Name</label>
              <input type="text" className="form-control" name="name" value={formData.name} onChange={handleChange} />
            </div>
            <div className="mb-3">
              <label className="form-label">Slug</label>
              <input type="text" className="form-control" name="slug" value={formData.slug} onChange={handleChange} />
            </div>
            <div className="mb-3">
              <label className="form-label">Description</label>
              <textarea className="form-control" name="description" value={formData.description} onChange={handleChange}></textarea>
            </div>
            <div className="mb-3">
              <label className="form-label">Module</label>
              <select className="form-select" name="module" value={formData.module} onChange={handleChange}>
                <option value="">Select Module</option>
                {modules.map((mod, i) => (
                  <option key={i} value={mod}>{mod}</option>
                ))}
              </select>
            </div>
            
            <div className="mb-3">
              <label className="form-label">Resource</label>
              <select className="form-select" name="resource" value={formData.resource} onChange={handleChange}>
                <option value="">Select Resource</option>
                {resources.map((res, i) => (
                  <option key={i} value={res}>{res}</option>
                ))}
              </select>
            </div>

            <div className="mb-3">
              <label className="form-label">Actions</label><br />
              {actions.map((act, i) => (
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
            <button type="submit" className="btn btn-primary">Create Permission</button>
          </form>

        

  {localPermissions.map((p, index) => (
  <div
    key={index}
    className="d-flex flex-wrap flex-md-nowrap align-items-start bg-light rounded px-2 py-2 mb-2"
    style={{ fontSize: '15px' }}
  >
    <div className="col-12 col-md-3 fw-semibold">{p.name}</div>
    <div className="col-12 col-md-2 text-muted"><code>{p.slug}</code></div>
    <div className="col-12 col-md-2">
      <span className="badge bg-primary">{p.action}</span>
    </div>
    <div className="col-12 col-md-2">{p.module}</div>
    <div className="col-12 col-md-2 d-flex gap-2">
      <button
        className="btn btn-sm btn-outline-secondary"
        onClick={() => handleEditPermission(index)}
      >
        <i className="bi bi-pencil-square"></i>
      </button>
      <button
        className="btn btn-sm btn-outline-danger"
        onClick={() => handleDeletePermission(p.slug)}
      >
        <i className="bi bi-trash"></i>
      </button>
    </div>
  </div>
))}



         
        </div>
      </div>
    </div>
  );
};

export default CreatePermission;
