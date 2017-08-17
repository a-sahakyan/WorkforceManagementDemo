using WorkforceManagement.VM.ViewModels;

namespace WorkforceManagement.BLL.Logic
{
    public interface ISkillLogic
    {
        void SaveSkills(SkillViewModel datas);

        bool Check(SkillViewModel skill);

        void UpdateSkills(SkillViewModel skills);
    }
}
