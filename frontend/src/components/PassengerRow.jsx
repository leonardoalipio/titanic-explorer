const PassengerRow = ({ passenger }) => {
  return (
    <tr>
    <td>{passenger.survived}</td>
    <td>{passenger.pclass}</td>
    <td>{passenger.name}</td>
    <td>{passenger.sex}</td>
    <td>{passenger.age}</td>
    <td>{passenger.sibSp}</td>
    <td>{passenger.parch}</td>
    <td>{passenger.fare}</td>
    </tr>
  );
}

export default PassengerRow;