import StandartInput from "../../inputs/StandartInput/StandartInput";
import cl from "./CreateReportDialog.module.scss";
import AccentButton from "../../buttons/AccentButton/AccentButton";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faClose } from "@fortawesome/free-solid-svg-icons";
import { useState } from "react";

const CreateReportDialog = ({ onCreate, error, onCancel, disabled }) => {
  const [reportTitle, setReportTitle] = useState("");
  const createProject = (e) => {
    e.preventDefault();
    if (!disabled) {
      onCreate(reportTitle);
    }
  };

  return (
    <div className={cl.dialog_container} onClick={onCancel}>
      <form
        className={cl.dialog}
        onClick={(e) => e.stopPropagation()}
        onSubmit={createProject}
      >
        <div className={cl.title}>
          <h3>Создать отчёт</h3>
          <FontAwesomeIcon icon={faClose} onClick={onCancel} />
        </div>
        <div className={cl.inputs}>
          <StandartInput
            disabled={disabled}
            placeholder="Название отчёта"
            onChange={(e) => setReportTitle(e.target.value)}
          />
        </div>
        <div className={cl.error}>
          <p>{error}</p>
        </div>
        <div className={cl.buttons}>
          <AccentButton disabled={disabled} type="submit">
            Создать
          </AccentButton>
        </div>
      </form>
    </div>
  );
};

export default CreateReportDialog;