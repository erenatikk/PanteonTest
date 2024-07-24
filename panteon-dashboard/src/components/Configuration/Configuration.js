import React, { useState, useEffect } from "react";
import "./Configuration.css";
import { getConfigurations, updateConfiguration } from "../../api";

const buildingTypes = [
  "Farm",
  "Academy",
  "Headquarters",
  "LumberMill",
  "Barracks",
];

const Configuration = () => {
  const [configurations, setConfigurations] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [buildingType, setBuildingType] = useState("");
  const [buildingCost, setBuildingCost] = useState("");
  const [constructionTime, setConstructionTime] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    const fetchConfigurations = async () => {
      try {
        const response = await getConfigurations();
        setConfigurations(response.data);
      } catch (error) {
        console.error("Error fetching configurations:", error);
      }
    };
    fetchConfigurations();
  }, []);

  const handleShowModal = (config) => {
    if (config) {
      setBuildingType(config.buildingType);
      setBuildingCost(config.buildingCost);
      setConstructionTime(config.constructionTime);
    } else {
      setBuildingType("");
      setBuildingCost("");
      setConstructionTime("");
    }
    setError("");
    setShowModal(true);
  };

  const handleUpdateConfiguration = async () => {
    if (parseInt(buildingCost) < 0) {
      setError("Building Cost cannot be negative");
      setShowErrorModal(true);
      return;
    }
    if (parseInt(constructionTime) < 30 || parseInt(constructionTime) > 1800) {
      setError("Construction Time must be between 30 and 1800");
      setShowErrorModal(true);
      return;
    }

    const updatedConfiguration = {
      buildingType,
      buildingCost: parseInt(buildingCost),
      constructionTime: parseInt(constructionTime) 
    };

    try {
      console.log("Güncelleme işlemi başladı", updatedConfiguration);
      const response = await updateConfiguration(updatedConfiguration);
      console.log("Güncelleme işlemi başarılı", response);
      setConfigurations((prevConfigs) =>
        prevConfigs.map((config) =>
          config.buildingType === buildingType ? response.data : config
        )
      );
      setShowModal(false);
      setBuildingType("");
      setBuildingCost("");
      setConstructionTime("");
      setError("");
    } catch (error) {
      console.error("Error updating configuration:", error);
    }
  };

  return (
    <div className="configuration-container">
      <button className="addButton" onClick={() => handleShowModal(null)}>
        Update Configuration
      </button>
      <div className="tableDiv">
        <table>
          <thead>
            <tr>
              <th>BuildingType</th>
              <th>BuildingCost</th>
              <th>ConstructionTime</th>
            </tr>
          </thead>
          <tbody>
            {configurations.map((config) => (
              <tr key={config._id} onClick={() => handleShowModal(config)}>
                <td>{config.buildingType}</td>
                <td>{config.buildingCost}</td>
                <td>{config.constructionTime}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>

      {showModal && (
        <div className="modal">
          <div className="modal-content">
            <h3>Edit Configuration</h3>
            <select
              value={buildingType}
              onChange={(e) => setBuildingType(e.target.value)}
              required
            >
              <option value="" disabled>
                Select Building Type
              </option>
              {buildingTypes.map((type) => (
                <option key={type} value={type}>
                  {type}
                </option>
              ))}
            </select>
            <input
              type="number"
              value={buildingCost}
              onChange={(e) => setBuildingCost(e.target.value)}
              placeholder="Building Cost"
              required
            />
            <input
              type="number"
              value={constructionTime}
              onChange={(e) => setConstructionTime(e.target.value)}
              placeholder="Construction Time"
              required
            />
            <button className="modalButton" onClick={handleUpdateConfiguration}>
              Update
            </button>
            <button className="modalButton" onClick={() => setShowModal(false)}>
              Cancel
            </button>
          </div>
        </div>
      )}

      {showErrorModal && (
        <div className="modal">
          <div className="modal-content">
            <h3>Error</h3>
            <p>{error}</p>
            <button className="modalButton" onClick={() => setShowErrorModal(false)}>
              Close
            </button>
          </div>
        </div>
      )}
    </div>
  );
};

export default Configuration;
