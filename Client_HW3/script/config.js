// localhost vs production server

const config = {
  isLocalhost: false, // Switch this to false for production
  PORT: "7067", // Your local port
  version: "tar3", // Switch API version here, number of tar

  // Base API without 'Games' at the end for more flexibility
  get baseApi() {
    return this.isLocalhost
      ? `https://localhost:${this.PORT}/api`
      : `https://proj.ruppin.ac.il/igroup10/test2/tar1/api/`;
  },

  // Helper method for getting API URLs
  getApiUrl(endpoint) {
    return `${this.baseApi}/${endpoint}`;
  },
};

window.config = config;
