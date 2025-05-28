console.log(process.env)

export const API_ENDPOINTS = {   
    LOGIN_ENDPOINT : `${process.env.REACT_APP_LOGIN_DOMAIN}/api/User/login`,
    USERPROFILE_ENDPOINT : `${process.env.REACT_APP_LOGIN_DOMAIN}/api/User/profile`,
    USERS_ENDPOINT : `${process.env.REACT_APP_LOGIN_DOMAIN}/api/User/GetUsers`,
    ROLES_ENDPOINT : `${process.env.REACT_APP_LOGIN_DOMAIN}/api/Role`,    
    ROLES_PAGES : `${process.env.REACT_APP_LOGIN_DOMAIN}/api/Pages`,
    ROLES_ACTIONS : `${process.env.REACT_APP_LOGIN_DOMAIN}/api/Actions`
}