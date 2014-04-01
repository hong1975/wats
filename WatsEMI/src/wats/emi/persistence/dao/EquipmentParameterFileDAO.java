package wats.emi.persistence.dao;

import org.hibernate.SessionFactory;
import wats.emi.persistence.SessionFactoryGenerator;
import wats.emi.persistence._EquipmentParameterFile;

/**
 * Created with IntelliJ IDEA.
 * User: haoyu
 * Date: 14-3-30
 * Time: 上午4:47
 * To change this template use File | Settings | File Templates.
 */
public class EquipmentParameterFileDAO extends GenericDAO<_EquipmentParameterFile> {
    public EquipmentParameterFileDAO() {
        super(SessionFactoryGenerator.getSessionFactory());
    }

    public EquipmentParameterFileDAO(GenericDAO dao) {
        super(dao);
    }
}
