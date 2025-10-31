import { useEffect, useState } from "react";
import PassengerRow from "./PassengerRow";

const PassengerList = () => {
  
  const [passengers, setPassengers] = useState([])
  
  useEffect(() => {
    const fetchPassengers = async () => {
      try {
        const response = await fetch("http://127.0.0.1:5012/passengers");
        const data = await response.json();
        setPassengers(data);
      } catch (error) {
        console.error("Error fetching passengers:", error);
      }
    }
    fetchPassengers();
  }, [])

  return (
    <div className="row g-5">
      <div className="col-md-8">
        <h3 className="pb-4 mb-4 fst-italic border-bottom">List of Passengers of RMS Titanic</h3>
        <article className="blog-post">
            
            <p>Select any filter to see passengers:</p>

            <table className="table">
              <thead>
                <tr>
                  <th>Survived</th>
                  <th>Class</th>
                  <th>Name</th>
                  <th>Sex</th>
                  <th>Age</th>
                  <th>SiblingsOrSpouse</th>
                  <th>ParentOrChildren</th>
                  <th>Fare</th>
                </tr>
              </thead>
              <tbody>
                {passengers.map(passenger => 
                  <PassengerRow key={passenger.id} passenger={passenger} />
                )}
              </tbody>
            </table>
            <p>
              This is some additional paragraph placeholder content. It's a
              slightly shorter version of the other highly repetitive body text
              used throughout.
            </p>
          </article>
      </div>
    </div>
  );
}

export default PassengerList;