import PassengerRow from "./PassengerRow";

const PassengerList = ({ passengers }) => {
  return (
      <article className="blog-post">
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
            {passengers.map((passenger, index) => 
              <PassengerRow key={index} passenger={passenger} />
            )}
          </tbody>
        </table>
        <p>
          This is some additional paragraph placeholder content. It's a
          slightly shorter version of the other highly repetitive body text
          used throughout.
        </p>
      </article>

  );
}

export default PassengerList;